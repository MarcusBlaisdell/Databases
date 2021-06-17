
CREATE TABLE Users (
	avg_stars		FLOAT,
	c_cool			INTEGER,
	c_cute			INTEGER, 
	c_funny		INTEGER, 
	c_hot			INTEGER,
	c_list			INTEGER,
	c_more			INTEGER,
	c_note			INTEGER,
	c_photos		INTEGER,
	c_plain		INTEGER,
	c_profile		INTEGER,
	c_writer		INTEGER,
	cool			INTEGER,
	fans			INTEGER,
	funny			INTEGER,
	name			VARCHAR(50),
	review_count		INTEGER,
	useful			INTEGER,
	user_id_num		VARCHAR(22) NOT NULL,
	yelping_since	DATE,
	PRIMARY KEY (user_id_num)
);


CREATE TABLE Review(
	review_id_num				VARCHAR(22) NOT NULL,
	user_id_num				VARCHAR(22) NOT NULL,
	business_id_num				VARCHAR(22) NOT NULL,
	stars					FLOAT,
	date					DATE,
	text					TEXT,
	useful					INTEGER,
	funny					INTEGER,
	cool					INTEGER,
	PRIMARY KEY(review_id_num),
	FOREIGN KEY (user_id_num) REFERENCES Users (user_id_num),
	FOREIGN KEY (business_id_num) REFERENCES Business (business_id_num)
);

CREATE TABLE Business(
	business_id_num				VARCHAR(22)	NOT NULL,
	name					VARCHAR(35),
	neighborhood				VARCHAR(30),
	street					VARCHAR(50),
	city					VARCHAR(30),
	state					VARCHAR(2),
	postal					INTEGER,
	latitude				FLOAT,
	longitude				FLOAT,
	avg_rating				FLOAT,
	review_count				INTEGER,
	is_open					BIT,
	num_checkins				INTEGER DEFAULT 0,
	review_rating				FLOAT DEFAULT 0.0,
	PRIMARY KEY(business_id_num)
);

CREATE TABLE WriteRespondReview(
	review_id_num			VARCHAR(22) NOT NULL,
	user_id_num				VARCHAR(22),
	business_id_num			VARCHAR(22),
	PRIMARY KEY(review_id_num),
	FOREIGN KEY(review_id_num)REFERENCES Review(review_id_num),
	FOREIGN KEY(business_id_num) REFERENCES Business(business_id_num),
	FOREIGN KEY(user_id_num) REFERENCES Users(user_id_num)
);

CREATE TABLE FavoriteBusiness(
	user_id_num				VARCHAR(22) NOT NULL,
	business_id_num			VARCHAR(22) NOT NULL,
	PRIMARY KEY(user_id_num,business_id_num),
	FOREIGN KEY(business_id_num) REFERENCES Business(business_id_num),
	FOREIGN KEY(user_id_num) REFERENCES Users(user_id_num)
);

CREATE TABLE CheckIn(
	business_id_num			VARCHAR(22) NOT NULL,
	day				VARCHAR (9),
	time_checkin			TIME,
	check_ins_num			INTEGER,
	PRIMARY KEY(business_id_num, day, time_checkin),
	FOREIGN KEY(business_id_num) REFERENCES Business(business_id_num)
);

CREATE TABLE HasAttribute(
	business_id_num				VARCHAR(22) NOT NULL,
	parent					VARCHAR(25) NOT NULL,
	attribute_name				VARCHAR(50) NOT NULL,
	value					VARCHAR (10),
	PRIMARY KEY(business_id_num,attribute_name,parent),
	FOREIGN KEY(business_id_num) REFERENCES Business(business_id_num)
);

CREATE TABLE Friend(
	user_id_num				VARCHAR(22) NOT NULL,
	friend_id_num			VARCHAR(22) NOT NULL,
	PRIMARY KEY(user_id_num,friend_id_num),
	FOREIGN KEY(user_id_num) REFERENCES Users(user_id_num),
	FOREIGN KEY(friend_id_num) REFERENCES Users(user_id_num)
);

CREATE TABLE Elite (
	user_id_num		VARCHAR(22),
	year			char(4),
	PRIMARY KEY (user_id_num, year),
	FOREIGN KEY (user_id_num) REFERENCES Users (user_id_num)
);

CREATE TABLE Category (
	business_id_num		VARCHAR (22),
	category_name 		VARCHAR (25),
	PRIMARY KEY (business_id_num, category_name),
	FOREIGN KEY (business_id_num) REFERENCES Business (business_id_num)
);

CREATE TABLE Hours (
	business_id_num		VARCHAR (22),
	day			VARCHAR (9),
	hours			varChar (11),
	PRIMARY KEY (business_id_num, day),
	FOREIGN KEY (business_id_num) REFERENCES Business (business_id_num)
);
