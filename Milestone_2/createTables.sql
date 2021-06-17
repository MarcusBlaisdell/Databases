CREATE TABLE Business (
	business_id		CHAR (22),
	name			VARCHAR(25) NOT NULL,
	neighborhood		VARCHAR(25),
	address			VARCHAR(50),
	city			VARCHAR(25),
	state			CHAR(2),
	postal_code		INTEGER,
	latitude		FLOAT,
	longitude		FLOAT,
	stars			FLOAT,
	review_count		INTEGER,
	is_open			BOOLEAN,
	numCheckins		INTEGER DEFAULT 0,
	reviewRating		FLOAT DEFAULT 0.0,
	PRIMARY KEY (business_id)
);

CREATE TABLE Attributes (
	parent		VARCHAR(15),
	attribute	VARCHAR(15) NOT NULL,
	value		VARCHAR(15),
	business_id	CHAR(22),
	PRIMARY KEY (attribute, business_id),
	FOREIGN KEY (business_id) REFERENCES Business (business_id)
);

CREATE TABLE Categories (
	category_name		VARCHAR(15) NOT NULL,
	business_id		CHAR(22),
	PRIMARY KEY (category_name, business_id),
	FOREIGN KEY (business_id) REFERENCES Business (business_id)
);

CREATE TABLE Hours (
	Day		VARCHAR(9),
	Open		VARCHAR(5) NOT NULL,
	Close		VARCHAR(5) NOT NULL,
	business_id	CHAR(22),
	PRIMARY KEY (Day, business_id),
	FOREIGN KEY (business_id) REFERENCES Business (business_id)
);
