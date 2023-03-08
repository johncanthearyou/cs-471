DROP TABLE transactionItems;

CREATE TABLE transactionItems
(
  transaction_id int,
  name text not null,
  quantity int not null
);