package types

import "fmt"

// TypeInfo describes a single Go type as understood by the mapper.
// It deliberately avoids raw strings outside the registry package.
type TypeInfo struct {
	Package string
	Name    string

	IsPointer  bool
	IsSlice    bool
	IsEnum     bool
	IsNullable bool

	Kind Kind
}

// String returns a human-readable representation of TypeInfo.
func (t TypeInfo) String() string {
	return fmt.Sprintf("{ %s %v %v %v %v %v}", t.Name, t.IsPointer, t.IsSlice, t.IsEnum, t.IsNullable, t.Kind)
}
