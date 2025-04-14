CREATE TABLE profile_preferences (
    id SERIAL PRIMARY KEY NOT NULL,
    profile_id SERIAL NOT NULL,
    characteristic_id SERIAL NOT NULL,
    CONSTRAINT fk_profile_preferences_profiles FOREIGN KEY (interest_id) REFERENCES profiles(id),
    CONSTRAINT fk_profile_preferences_characteristic FOREIGN KEY (user_id) REFERENCES characteristics(id)
);