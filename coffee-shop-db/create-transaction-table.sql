DROP TABLE transactions;

CREATE TABLE transactions
(
  id INTEGER primary key AUTOINCREMENT,
  payment text,
  total_cost float not null,
  date datetime DEFAULT CURRENT_TIMESTAMP,
  customer_name text not null,
  complete boolean DEFAULT 0
);