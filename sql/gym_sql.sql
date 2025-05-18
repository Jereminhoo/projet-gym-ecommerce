--
-- PostgreSQL database dump
--

-- Dumped from database version 17.2
-- Dumped by pg_dump version 17.2

-- Started on 2025-05-18 12:08:03

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: -
--

CREATE SCHEMA public;


--
-- TOC entry 4899 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: -
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 220 (class 1259 OID 16805)
-- Name: categorie; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.categorie (
    id_cat integer NOT NULL,
    nom_cat character varying(50) NOT NULL,
    description_cat character varying(50)
);


--
-- TOC entry 219 (class 1259 OID 16804)
-- Name: categorie_id_cat_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.categorie_id_cat_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4900 (class 0 OID 0)
-- Dependencies: 219
-- Name: categorie_id_cat_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.categorie_id_cat_seq OWNED BY public.categorie.id_cat;


--
-- TOC entry 224 (class 1259 OID 16826)
-- Name: commande; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.commande (
    id_com integer NOT NULL,
    date_com timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    id_util integer NOT NULL
);


--
-- TOC entry 223 (class 1259 OID 16825)
-- Name: commande_id_com_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.commande_id_com_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4901 (class 0 OID 0)
-- Dependencies: 223
-- Name: commande_id_com_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.commande_id_com_seq OWNED BY public.commande.id_com;


--
-- TOC entry 225 (class 1259 OID 16838)
-- Name: detail; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.detail (
    id_com integer NOT NULL,
    id_prod integer NOT NULL,
    quantite integer NOT NULL,
    total_prix_unit numeric(15,2) NOT NULL,
    CONSTRAINT detail_quantite_check CHECK ((quantite > 0)),
    CONSTRAINT detail_total_prix_unit_check CHECK ((total_prix_unit >= (0)::numeric))
);


--
-- TOC entry 222 (class 1259 OID 16812)
-- Name: produit; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.produit (
    id_prod integer NOT NULL,
    nom_produit character varying(50) NOT NULL,
    description character varying(50),
    stock integer NOT NULL,
    prix numeric(15,2) NOT NULL,
    id_cat integer NOT NULL,
    photo character varying(255),
    CONSTRAINT produit_prix_check CHECK ((prix > (0)::numeric)),
    CONSTRAINT produit_stock_check CHECK ((stock >= 0))
);


--
-- TOC entry 221 (class 1259 OID 16811)
-- Name: produit_id_prod_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.produit_id_prod_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4902 (class 0 OID 0)
-- Dependencies: 221
-- Name: produit_id_prod_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.produit_id_prod_seq OWNED BY public.produit.id_prod;


--
-- TOC entry 218 (class 1259 OID 16795)
-- Name: utilisateur; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.utilisateur (
    id_util integer NOT NULL,
    nom_util character varying(50) NOT NULL,
    prenom_util character varying(50) NOT NULL,
    mdp character varying(50) NOT NULL,
    mail character varying(50) NOT NULL,
    admin boolean DEFAULT false
);


--
-- TOC entry 217 (class 1259 OID 16794)
-- Name: utilisateur_id_util_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.utilisateur_id_util_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4903 (class 0 OID 0)
-- Dependencies: 217
-- Name: utilisateur_id_util_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.utilisateur_id_util_seq OWNED BY public.utilisateur.id_util;


--
-- TOC entry 4716 (class 2604 OID 16808)
-- Name: categorie id_cat; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.categorie ALTER COLUMN id_cat SET DEFAULT nextval('public.categorie_id_cat_seq'::regclass);


--
-- TOC entry 4718 (class 2604 OID 16829)
-- Name: commande id_com; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.commande ALTER COLUMN id_com SET DEFAULT nextval('public.commande_id_com_seq'::regclass);


--
-- TOC entry 4717 (class 2604 OID 16815)
-- Name: produit id_prod; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.produit ALTER COLUMN id_prod SET DEFAULT nextval('public.produit_id_prod_seq'::regclass);


--
-- TOC entry 4714 (class 2604 OID 16798)
-- Name: utilisateur id_util; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.utilisateur ALTER COLUMN id_util SET DEFAULT nextval('public.utilisateur_id_util_seq'::regclass);


--
-- TOC entry 4888 (class 0 OID 16805)
-- Dependencies: 220
-- Data for Name: categorie; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.categorie (id_cat, nom_cat, description_cat) VALUES (1, 'Haltères', 'Poids pour la musculation');
INSERT INTO public.categorie (id_cat, nom_cat, description_cat) VALUES (2, 'Protéines', 'Compléments alimentaires protéinés');


--
-- TOC entry 4892 (class 0 OID 16826)
-- Dependencies: 224
-- Data for Name: commande; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.commande (id_com, date_com, id_util) VALUES (1, '2025-05-01 10:30:00', 1);
INSERT INTO public.commande (id_com, date_com, id_util) VALUES (2, '2025-05-02 14:15:00', 2);


--
-- TOC entry 4893 (class 0 OID 16838)
-- Dependencies: 225
-- Data for Name: detail; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.detail (id_com, id_prod, quantite, total_prix_unit) VALUES (1, 1, 2, 51.98);
INSERT INTO public.detail (id_com, id_prod, quantite, total_prix_unit) VALUES (2, 2, 1, 39.90);


--
-- TOC entry 4890 (class 0 OID 16812)
-- Dependencies: 222
-- Data for Name: produit; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.produit (id_prod, nom_produit, description, stock, prix, id_cat, photo) VALUES (1, 'Haltère 10kg', 'Haltère en fonte de 10Kg', 20, 25.99, 1, 'halteres1.jpg');
INSERT INTO public.produit (id_prod, nom_produit, description, stock, prix, id_cat, photo) VALUES (2, 'Protéine Whey', 'Whey vanille 1kg, 87g de prot pour 100g', 15, 39.90, 2, 'proteines1.jpg');
INSERT INTO public.produit (id_prod, nom_produit, description, stock, prix, id_cat, photo) VALUES (3, 'Haltère 5kg', 'Haltère en fonte de 5Kg', 25, 15.99, 1, 'halteres2.jpg');
INSERT INTO public.produit (id_prod, nom_produit, description, stock, prix, id_cat, photo) VALUES (4, 'Haltère 15kg', 'Haltère en fonte de 15Kg', 18, 29.99, 1, 'halteres3.jpg');
INSERT INTO public.produit (id_prod, nom_produit, description, stock, prix, id_cat, photo) VALUES (5, 'Haltère réglable', 'Haltère réglable de 2 à 24Kg', 12, 69.99, 1, 'halteres4.jpg');
INSERT INTO public.produit (id_prod, nom_produit, description, stock, prix, id_cat, photo) VALUES (6, 'Paire d''haltères 2kg', 'Paire de 2Kg idéale pour les échauffements', 40, 9.90, 1, 'halteres5.jpg');
INSERT INTO public.produit (id_prod, nom_produit, description, stock, prix, id_cat, photo) VALUES (7, 'Protéine Chocolat', 'Whey chocolat 1kg, 85g de prot pour 100g', 10, 39.90, 2, 'proteines2.jpg');
INSERT INTO public.produit (id_prod, nom_produit, description, stock, prix, id_cat, photo) VALUES (8, 'Protéine Vegan', 'Protéine végétale 750g, sans lactose', 20, 34.90, 2, 'proteines3.jpg');
INSERT INTO public.produit (id_prod, nom_produit, description, stock, prix, id_cat, photo) VALUES (9, 'Protéine Caséine', 'Caséine micellaire 1kg, digestion lente', 15, 42.00, 2, 'proteines4.jpg');
INSERT INTO public.produit (id_prod, nom_produit, description, stock, prix, id_cat, photo) VALUES (10, 'Isolate 90%', 'Protéine isolate 90%, 1kg', 8, 44.90, 2, 'proteines5.jpg');


--
-- TOC entry 4886 (class 0 OID 16795)
-- Dependencies: 218
-- Data for Name: utilisateur; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.utilisateur (id_util, nom_util, prenom_util, mdp, mail, admin) VALUES (1, 'Dupont', 'Jean', 'motdepasse123', 'jean.dupont@email.com', false);
INSERT INTO public.utilisateur (id_util, nom_util, prenom_util, mdp, mail, admin) VALUES (2, 'Martin', 'Claire', 'azerty789', 'claire.martin@email.com', false);
INSERT INTO public.utilisateur (id_util, nom_util, prenom_util, mdp, mail, admin) VALUES (3, 'Jeremy', 'Muanza', 'jeremy', 'jeremy@gmail.com', true);
INSERT INTO public.utilisateur (id_util, nom_util, prenom_util, mdp, mail, admin) VALUES (4, 'Mathieu', 'Colle', 'matcolle', 'mathieu@gmail.com', true);


--
-- TOC entry 4904 (class 0 OID 0)
-- Dependencies: 219
-- Name: categorie_id_cat_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.categorie_id_cat_seq', 2, true);


--
-- TOC entry 4905 (class 0 OID 0)
-- Dependencies: 223
-- Name: commande_id_com_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.commande_id_com_seq', 2, true);


--
-- TOC entry 4906 (class 0 OID 0)
-- Dependencies: 221
-- Name: produit_id_prod_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.produit_id_prod_seq', 10, true);


--
-- TOC entry 4907 (class 0 OID 0)
-- Dependencies: 217
-- Name: utilisateur_id_util_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.utilisateur_id_util_seq', 6, true);


--
-- TOC entry 4729 (class 2606 OID 16810)
-- Name: categorie categorie_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.categorie
    ADD CONSTRAINT categorie_pkey PRIMARY KEY (id_cat);


--
-- TOC entry 4733 (class 2606 OID 16832)
-- Name: commande commande_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.commande
    ADD CONSTRAINT commande_pkey PRIMARY KEY (id_com);


--
-- TOC entry 4735 (class 2606 OID 16844)
-- Name: detail detail_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.detail
    ADD CONSTRAINT detail_pkey PRIMARY KEY (id_com, id_prod);


--
-- TOC entry 4731 (class 2606 OID 16819)
-- Name: produit produit_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.produit
    ADD CONSTRAINT produit_pkey PRIMARY KEY (id_prod);


--
-- TOC entry 4725 (class 2606 OID 16803)
-- Name: utilisateur utilisateur_mail_key; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.utilisateur
    ADD CONSTRAINT utilisateur_mail_key UNIQUE (mail);


--
-- TOC entry 4727 (class 2606 OID 16801)
-- Name: utilisateur utilisateur_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.utilisateur
    ADD CONSTRAINT utilisateur_pkey PRIMARY KEY (id_util);


--
-- TOC entry 4737 (class 2606 OID 16833)
-- Name: commande commande_id_util_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.commande
    ADD CONSTRAINT commande_id_util_fkey FOREIGN KEY (id_util) REFERENCES public.utilisateur(id_util) ON DELETE CASCADE;


--
-- TOC entry 4738 (class 2606 OID 16845)
-- Name: detail detail_id_com_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.detail
    ADD CONSTRAINT detail_id_com_fkey FOREIGN KEY (id_com) REFERENCES public.commande(id_com) ON DELETE CASCADE;


--
-- TOC entry 4739 (class 2606 OID 16850)
-- Name: detail detail_id_prod_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.detail
    ADD CONSTRAINT detail_id_prod_fkey FOREIGN KEY (id_prod) REFERENCES public.produit(id_prod) ON DELETE CASCADE;


--
-- TOC entry 4736 (class 2606 OID 16820)
-- Name: produit produit_id_cat_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.produit
    ADD CONSTRAINT produit_id_cat_fkey FOREIGN KEY (id_cat) REFERENCES public.categorie(id_cat) ON DELETE SET NULL;


-- Completed on 2025-05-18 12:08:03

--
-- PostgreSQL database dump complete
--

