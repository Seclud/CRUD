PGDMP     2    $                {            test_db    15.2    15.2     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    16399    test_db    DATABASE     {   CREATE DATABASE test_db WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE test_db;
                postgres    false            �            1259    16458    product    TABLE     k   CREATE TABLE public.product (
    id integer NOT NULL,
    name character varying(50),
    cost numeric
);
    DROP TABLE public.product;
       public         heap    postgres    false            �            1259    16457    product_id_seq    SEQUENCE     �   CREATE SEQUENCE public.product_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.product_id_seq;
       public          postgres    false    215            �           0    0    product_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.product_id_seq OWNED BY public.product.id;
          public          postgres    false    214            e           2604    16461 
   product id    DEFAULT     h   ALTER TABLE ONLY public.product ALTER COLUMN id SET DEFAULT nextval('public.product_id_seq'::regclass);
 9   ALTER TABLE public.product ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    214    215    215            �          0    16458    product 
   TABLE DATA           1   COPY public.product (id, name, cost) FROM stdin;
    public          postgres    false    215   N
       �           0    0    product_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.product_id_seq', 41, true);
          public          postgres    false    214            g           2606    16465    product product_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.product
    ADD CONSTRAINT product_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.product DROP CONSTRAINT product_pkey;
       public            postgres    false    215            �   b   x���
�0E�{?F�'mg��9ХK�)�@�޸�s8�5ֈRӱ���>�c�xC���?��§-�Ԅ��v�}(ޡ�܄���:~:��i�     