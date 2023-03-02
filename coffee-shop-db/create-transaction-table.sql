CREATE TABLE transactions
(
  id INTEGER primary key AUTOINCREMENT,
  drinks json,
  food json,
  payment text,
  date datetime DEFAULT getdate,
  customer_name text not null
);