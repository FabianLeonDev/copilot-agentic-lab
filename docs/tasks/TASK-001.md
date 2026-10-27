# TASK-001: Product stock validation

## Objective

Implement a Product entity with stock validation.

## Requirements

A Product must contain:

- Id
- Name
- Stock

## Business rules

- Product name cannot be empty.
- Initial stock cannot be negative.
- Adding stock requires a positive quantity.
- Removing stock requires a positive quantity.
- Stock cannot become negative.

## Acceptance criteria

1. A valid Product can be created.
2. Invalid product names are rejected.
3. Negative initial stock is rejected.
4. Adding stock increases the quantity.
5. Removing stock decreases the quantity.
6. Removing more than available stock is rejected.
7. Rejected operations preserve the existing stock.
8. Unit tests cover the business rules.

## Scope

Allowed:
- Domain model changes.
- Unit tests.

Not allowed:
- New dependencies.
- Database persistence.
- HTTP endpoints.
- Architecture changes.

## Definition of Done

- Project builds.
- Unit tests pass.
- QA completes independent review.
- Human reviews and accepts the result.