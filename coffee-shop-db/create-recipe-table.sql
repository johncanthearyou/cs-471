DROP TABLE recipe;
CREATE TABLE recipe
(
    id Integer primary key AUTOINCREMENT,
    name text not null,
    price float not null,
    ingredients text not null
);