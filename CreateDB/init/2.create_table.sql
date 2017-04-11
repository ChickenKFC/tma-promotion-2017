-- Project Name : noname
-- Date/Time    : 3/28/2017 6:46:30 AM
-- Author       : Trau Dai Ca
-- RDBMS Type   : PostgreSQL
-- Application  : A5:SQL Mk-2

-- sort_description
create table sort_description (
  book_id integer not null
  , sort_description character varying
  , delete_flag BOOLEAN default FALSE
  , constraint sort_description_PKC primary key (book_id)
) ;

-- search_history
create table search_history (
  search_history_id SERIAL not null
  , search_target_table integer
  , search_target_column integer
  , search_condition character varying
  , from_date timestamp
  , to_date timestamp
  , delete_flag BOOLEAN default FALSE
  , constraint search_history_PKC primary key (search_history_id)
) ;

-- barcodes
create table barcodes (
  book_id integer not null
  , bar_code character varying
  , constraint barcodes_PKC primary key (book_id)
) ;

-- scientific
create table scientific (
  book_id integer not null
  , book_name character varying
  , constraint scientific_PKC primary key (book_id)
) ;

-- mathematics
create table mathematics (
  book_id integer not null
  , book_name character varying
  , constraint mathematics_PKC primary key (book_id)
) ;

-- physical
create table physical (
  book_id integer not null
  , book_name character varying
  , constraint physical_PKC primary key (book_id)
) ;

-- books
create table books (
  book_id SERIAL not null
  --, book_type integer
  , book_shelf integer
  , pulish_date timestamp default statement_timestamp()
  , author character varying
  , delete_flag BOOLEAN default FALSE
  , constraint books_PKC primary key (book_id)
) ;

comment on table sort_description is 'sort_description';
comment on column sort_description.book_id is 'Book Id	 Book ID';
comment on column sort_description.sort_description is 'Sort Description';
comment on column sort_description.delete_flag is 'Delete Flag	 TRUE: already deleted.
FALSE: still available';

comment on table search_history is 'search_history';
comment on column search_history.search_history_id is 'Search History Id	 Book ID';
comment on column search_history.search_target_table is 'Search Target Table	 Private: 0
Public: 1';
comment on column search_history.search_target_column is 'Search Target Column';
comment on column search_history.search_condition is 'Search Condition';
comment on column search_history.from_date is 'From Date';
comment on column search_history.to_date is 'To Date';
comment on column search_history.delete_flag is 'Delete Flag	 TRUE: already deleted.
FALSE: still available';

comment on table barcodes is 'barcodes';
comment on column barcodes.book_id is 'Book ID';
comment on column barcodes.bar_code is 'Bar Code';

comment on table scientific is 'scientific';
comment on column scientific.book_id is 'Book ID';
comment on column scientific.book_name is 'Book Name';

comment on table mathematics is 'mathematics';
comment on column mathematics.book_id is 'Book ID';
comment on column mathematics.book_name is 'Book Name';

comment on table physical is 'physical';
comment on column physical.book_id is 'Book ID';
comment on column physical.book_name is 'Book Name';

comment on column books.book_shelf is 'Book Shelf';
comment on column books.pulish_date is 'Pushlish Date';
comment on column books.author is 'Author';
comment on column books.delete_flag is 'Delete Flag	 TRUE: already deleted.
FALSE: still available';
