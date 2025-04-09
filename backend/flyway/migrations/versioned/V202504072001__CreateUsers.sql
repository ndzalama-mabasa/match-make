CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE "users" (
  "id" UUID PRIMARY KEY NOT NULL DEFAULT gen_random_uuid(),
  "oauth_id" VARCHAR UNIQUE NOT NULL,
  "inactive" BOOLEAN NOT NULL DEFAULT false,
  "display_name" VARCHAR NOT NULL,
  "bio" VARCHAR,
  "avatar_url" VARCHAR,
  "species_id" INT NOT NULL,
  "planet_id" INT NOT NULL,
  FOREIGN KEY ("species_id") REFERENCES "species" ("id"),
  FOREIGN KEY ("planet_id") REFERENCES "planets" ("id")
);