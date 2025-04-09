CREATE TABLE "hobbies" (
  "id" SERIAL PRIMARY KEY NOT NULL,
  "hobby" VARCHAR NOT NULL,
  "user_id" UUID NOT NULL,
  FOREIGN KEY ("user_id") REFERENCES "users" ("id")
);