CREATE TABLE "reactions" (
  "id" SERIAL PRIMARY KEY NOT NULL,
  "reactor_id" UUID NOT NULL,
  "target_id" UUID NOT NULL,
  "is_positive" BOOLEAN NOT NULL DEFAULT true,
  FOREIGN KEY ("reactor_id") REFERENCES "users" ("id"),
  FOREIGN KEY ("target_id") REFERENCES "users" ("id")
);