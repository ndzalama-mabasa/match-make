CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE "contacts" (
  "id" UUID PRIMARY KEY NOT NULL DEFAULT gen_random_uuid(),
  "user_id" UUID NOT NULL,
  "messaging_platform_provider" VARCHAR NOT NULL,
  "messaging_platform_username" VARCHAR NOT NULL,
  FOREIGN KEY ("user_id") REFERENCES "users" ("id")
);