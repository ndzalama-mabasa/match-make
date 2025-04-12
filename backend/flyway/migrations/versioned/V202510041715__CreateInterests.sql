CREATE TABLE interests (
    id SERIAL PRIMARY KEY,
    interest_id INT NOT NULL,
    profile_id INT NOT NULL,
    CONSTRAINT fk_interests_profile FOREIGN KEY (profile_id) REFERENCES profiles(id),
    CONSTRAINT fk_interests_interest FOREIGN KEY (interest_id) REFERENCES interests(id)
);