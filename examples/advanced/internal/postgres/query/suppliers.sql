-- name: CreateSupplier :one
INSERT INTO schm_pos.suppliers(tenant_id, code, name, phone, email, address, created_at)
VALUES ($1, $2, $3, $4, $5, $6, NOW()) RETURNING *;

-- name: UpdateSupplier :one
UPDATE schm_pos.suppliers
SET
    tenant_id = COALESCE(sqlc.narg(tenant_id), tenant_id),
    code = COALESCE(sqlc.narg(code), code),
    name = COALESCE(sqlc.narg(name), name),
    phone = COALESCE(sqlc.narg(phone), phone),
    email = COALESCE(sqlc.narg(email), email),
    address = COALESCE(sqlc.narg(address), address),
    updated_at = NOW()
WHERE id = sqlc.arg(id) RETURNING *;

-- name: DeleteSupplier :one
UPDATE schm_pos.suppliers
SET
    updated_at = NOW(),
    deleted_at = NOW()
WHERE id = sqlc.arg(id) RETURNING *;

-- name: RestoreSupplier :one
UPDATE schm_pos.suppliers
SET 
    updated_at = NOW(),
    deleted_at = NULL
WHERE id = sqlc.arg(id) RETURNING *;


-- name: ListSupplier :many
SELECT
    id,
    tenant_id,
    code,
    name,
    phone,
    email,
    address,
    created_at,
    updated_at
FROM schm_pos.suppliers
WHERE
    deleted_at IS NULL
    AND(
        sqlc.narg('tenant_id')::uuid IS NULL
        OR tenant_id = sqlc.narg('tenant_id')::uuid
    )
ORDER BY name;


-- name: GetSupplier :one
SELECT
    id,
    tenant_id,
    code,
    name,
    phone,
    email,
    address,
    created_at,
    updated_at
FROM schm_pos.suppliers
WHERE
    id = sqlc.arg('id') AND deleted_at iS NULL;



-- name: GetSupplierByCode :one
SELECT * FROM schm_pos.suppliers
WHERE code = sqlc.arg('code') AND deleted_at IS NULL;