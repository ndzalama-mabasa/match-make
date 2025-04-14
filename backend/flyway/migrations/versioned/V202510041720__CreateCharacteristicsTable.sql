CREATE TABLE IF NOT EXISTS characteristics (
   id SERIAL PRIMARY KEY NOT NULL,
   characteristic_categories_id VARCHAR NOT NULL,
   characteristics_name VARCHAR NOT NULL,
   CONSTRAINT fk_characteristics_characteristic_categories FOREIGN KEY (characteristic_categories_id) REFERENCES characteristic_categories(id)
);
