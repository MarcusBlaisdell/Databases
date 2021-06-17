-- Table: public.similarity

-- DROP TABLE public.similarity;

CREATE TABLE public.similarity
(
	bus1		VARCHAR(22) NOT NULL,
	bus2		VARCHAR(22) NOT NULL,
	similarity	double precision NOT NULL,
	PRIMARY KEY(bus1,bus2),
	FOREIGN KEY(bus1) REFERENCES business(business_id_num),
	FOREIGN KEY(bus2) REFERENCES business(business_id_num)
)

WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.similarity
    OWNER to postgres;