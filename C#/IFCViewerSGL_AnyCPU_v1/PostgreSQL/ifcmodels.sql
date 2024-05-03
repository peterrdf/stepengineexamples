-- Table: public.models

-- DROP TABLE public.models;

CREATE TABLE public.models
(
    name text COLLATE pg_catalog."default" NOT NULL,
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    CONSTRAINT models_pkey PRIMARY KEY (name)
)

TABLESPACE pg_default;

ALTER TABLE public.models
    OWNER to postgres;
    
-- Table: public.instances

-- DROP TABLE public.instances;

CREATE TABLE public.instances
(
    model_id integer NOT NULL,
    name text COLLATE pg_catalog."default",
    description text COLLATE pg_catalog."default",
    global_id text COLLATE pg_catalog."default",
    vertices text COLLATE pg_catalog."default",
    indices text COLLATE pg_catalog."default"
)

TABLESPACE pg_default;

ALTER TABLE public.instances
    OWNER to postgres;