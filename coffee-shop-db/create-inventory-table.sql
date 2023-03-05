CREATE TABLE inventory
(
  id INTEGER primary key AUTOINCREMENT,
  name text not null,
  quantity int not null,
  size string not null,
  price float not null
);