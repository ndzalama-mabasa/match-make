CREATE TABLE IF NOT EXISTS characteristics (
   id SERIAL PRIMARY KEY NOT NULL,
   characteristic_category_id VARCHAR NOT NULL,
   characteristics_name VARCHAR NOT NULL
);
