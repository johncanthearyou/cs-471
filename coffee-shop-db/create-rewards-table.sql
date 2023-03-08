CREATE TABLE rewards
(
    id Integer primary key AUTOINCREMENT,
    customer_name text not null,
    phone_number text not null,
    email text,
    drinks_until_free int not null DEFAULT 10
)