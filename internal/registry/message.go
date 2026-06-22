package registry

import (
	"fmt"
	"strings"

	"github.com/jwart212/protoc-gen-go-mapper/pkg/converter"
	"github.com/jwart212/protoc-gen-go-mapper/pkg/types"
)

// MessageConverter handles Message-to-Message conversions (nested messages).
type MessageConverter struct{}

// Match returns true for Message-to-Message conversions.
func (c MessageConverter) Match(src, dst types.TypeInfo) bool {
	// Strip package prefixes for comparison
	srcName := stripPackagePrefix(src.Name)
	dstName := stripPackagePrefix(dst.Name)

	// Match if both are messages and have the same type name (after stripping package prefix)
	return src.Kind == types.KindMessage && dst.Kind == types.KindMessage && srcName == dstName
}

// Priority returns a low priority for generic message conversions.
func (c MessageConverter) Priority() int {
	return 5
}

// Generate emits a pass-through expression for message types (same type on both sides).
func (c MessageConverter) Generate(field converter.MappingField) (string, error) {
	// Strip package prefixes from type names for comparison
	srcName := stripPackagePrefix(field.SourceType.Name)
	dstName := stripPackagePrefix(field.TargetType.Name)

	// For message-to-message conversions with the same type, pass through directly
	if srcName == dstName {
		return field.SourceExpr, nil
	}
	return "", fmt.Errorf("unsupported message conversion: %v -> %v", field.SourceType, field.TargetType)
}

// stripPackagePrefix removes the protobuf package prefix from a type name.
// e.g., ".pos.supplier.v1.Suppliers" -> "Suppliers"
// Also handles leading spaces: " .pos.supplier.v1.Suppliers" -> "Suppliers"
func stripPackagePrefix(name string) string {
	// Trim any leading/trailing whitespace
	name = strings.TrimSpace(name)

	if strings.HasPrefix(name, ".") {
		parts := strings.Split(name, ".")
		if len(parts) > 0 {
			return strings.TrimSpace(parts[len(parts)-1])
		}
	}
	return name
}
