CREATE TABLE transactions
(
  id INTEGER primary key AUTOINCREMENT,
  drinks json,
  food json,
  payment text,
  total_cost float not null,
  date datetime DEFAULT CURRENT_TIMESTAMP,
  customer_name text not null,
  complete boolean DEFAULT 0
);