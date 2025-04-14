CREATE TABLE IF NOT EXISTS characteristics (
   id SERIAL PRIMARY KEY NOT NULL,
   characteristics_category VARCHAR NOT NULL,
   characteristics_name VARCHAR NOT NULL
);


INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Species', 'Andromedan');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Species', 'Sirian');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Species', 'Orion');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Species', 'Pleiadian');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Species', 'Zetan');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Species', 'Reptilian');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Species', 'Insectoid');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Species', 'Plant-Based');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Species', 'Energy Being');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Species', 'Aquatic Species');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Planet Type', 'Gas Giant');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Planet Type', 'Terrestrial (Rocky)');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Planet Type', 'Water World');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Planet Type', 'Desert Planet');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Planet Type', 'Ice Planet');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Planet Type', 'Jungle/Forest Planet');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Planet Type', 'Artificial Habitat');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Physical Characteristics', 'Preferred Min Appendages');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Physical Characteristics', 'Preferred Max Appendages');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Physical Characteristics', 'Preferred Min Height (Galactic Standard Units)');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Physical Characteristics', 'Preferred Max Height (Galactic Standard Units)');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Eye Configuration', 'Binocular (Two Eyes)');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Eye Configuration', 'Multiple Eyes');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Eye Configuration', 'Compound Eyes');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Eye Configuration', 'No Visible Eyes');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Skin/Exoskeleton Texture', 'Smooth');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Skin/Exoskeleton Texture', 'Scaly');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Skin/Exoskeleton Texture', 'Furry');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Skin/Exoskeleton Texture', 'Chitinous');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Skin/Exoskeleton Texture', 'Metallic');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Skin/Exoskeleton Texture', 'Gaseous');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Coloration', 'Monochromatic');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Coloration', 'Bioluminescent');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Coloration', 'Iridescent');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Coloration', 'Camouflaged');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Distinguishing Features', 'Antennae');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Distinguishing Features', 'Wings');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Distinguishing Features', 'Prehensile Tails');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Distinguishing Features', 'Telepathic Abilities');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Distinguishing Features', 'Psionic Powers');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Distinguishing Features', 'Gills');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Distinguishing Features', 'Internal Organs Visible');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Temperament', 'Calm');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Temperament', 'Energetic');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Temperament', 'Curious');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Temperament', 'Stoic');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Temperament', 'Humorous');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Temperament', 'Passionate');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Temperament', 'Logical');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Temperament', 'Spiritual');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Temperament', 'Mischievous');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Social Tendencies', 'Introverted');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Social Tendencies', 'Extroverted');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Social Tendencies', 'Ambiverted');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Social Tendencies', 'Prefers Small Groups');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Social Tendencies', 'Enjoys Large Gatherings');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Sense of Humor', 'Dry');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Sense of Humor', 'Slapstick');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Sense of Humor', 'Absurdist');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Sense of Humor', 'Intellectual');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Sense of Humor', 'Dark');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Interstellar Travel Methods', 'Wormhole Jumping');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Interstellar Travel Methods', 'Hyperspace Navigation');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Interstellar Travel Methods', 'Sublight Cruising');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Interstellar Travel Methods', 'Teleportation');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Interstellar Travel Methods', 'Stargates');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Recreational Activities', 'Gravitational Sports');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Recreational Activities', 'Zero-G Acrobatics');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Recreational Activities', 'Collecting Planetary Artifacts');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Recreational Activities', 'Xeno-Linguistics');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Recreational Activities', 'Cosmic Photography');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Recreational Activities', 'Virtual Reality Simulations');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Recreational Activities', 'Interdimensional Gaming');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Recreational Activities', 'Studying Ancient Earth Culture');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Dietary Preferences', 'Herbivorous');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Dietary Preferences', 'Carnivorous');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Dietary Preferences', 'Omnivorous');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Dietary Preferences', 'Energy Consumption');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Dietary Preferences', 'Cosmic Dust Connoisseur');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Dietary Preferences', 'Nutrient Paste Enthusiast');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Preferred Living Environment', 'Spacecraft/Station');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Preferred Living Environment', 'Urban Center');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Preferred Living Environment', 'Rural/Wilderness');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Preferred Living Environment', 'Underwater Colonies');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Preferred Living Environment', 'Underground Cities');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Relationship Preferences', 'Monogamous');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Relationship Preferences', 'Polyamorous');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Relationship Preferences', 'Open Relationships');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Relationship Preferences', 'Seeking Companionship');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Relationship Preferences', 'Seeking Deep Connection');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Tech Compatibility', 'Primitive Tech');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Tech Compatibility', 'Equivalent Tech');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Tech Compatibility', 'More Advanced Tech');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Tech Compatibility', 'Tech Agnostic');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Views on Inter-Species Relations', 'Positive');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Views on Inter-Species Relations', 'Neutral');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Views on Inter-Species Relations', 'Cautious');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Preferred Communication Method', 'Verbal/Auditory');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Preferred Communication Method', 'Telepathic');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Preferred Communication Method', 'Visual/Holographic');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Preferred Communication Method', 'Chemical Signals');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Preferred Communication Method', 'Binary Code');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Age Range Preference', 'Young (e.g., 0-100 Galactic Cycles)');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Age Range Preference', 'Adolescent (e.g., 101-300 Galactic Cycles)');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Age Range Preference', 'Adult (e.g., 301-800 Galactic Cycles)');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Age Range Preference', 'Elder (e.g., 801+ Galactic Cycles)');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Planet', 'Aqueous Prime');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Planet', 'Terra Nova');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Planet', 'Xylos');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Planet', 'Veridia');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Planet', 'Volcanus');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Planet', 'Glacia');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Planet', 'Aerilon');

INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Gender', 'Male');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Gender', 'Female');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Gender', 'Non-binary');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Gender', 'Fluid');
INSERT INTO characteristics (characteristics_category, characteristics_name) VALUES ('Gender', 'Agender');