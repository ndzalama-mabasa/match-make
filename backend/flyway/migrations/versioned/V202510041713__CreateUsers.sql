CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid() NOT NULL,
    oauth_id VARCHAR UNIQUE NOT NULL,
    inactive BOOLEAN DEFAULT false NOT NULL
);