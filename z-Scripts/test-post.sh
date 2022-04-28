curl --request POST \
--header "Content-Type: application/json" \
--data '{"id":1,"merchantId":1,"card":{"number":"CardNumber","expiry":{"month":2,"day":4},"currency":"GBP","cvv":128},"amount":1.2}' \
localhost:5000/payment && echo
