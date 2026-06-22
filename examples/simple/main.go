package main

import (
	"database/sql"
	"fmt"

	"github.com/jwart212/protoc-gen-go-mapper/examples/simple/db"
)

func main() {
	// Create a DB user
	dbUser := db.User{
		ID:     1,
		Name:   "John Doe",
		Email:  "john@example.com",
		Age:    sql.NullInt32{Int32: 30, Valid: true},
		Active: sql.NullBool{Bool: true, Valid: true},
	}
	fmt.Printf("DB User: %+v\n", dbUser)

	fmt.Println("Simple example completed successfully!")
}
