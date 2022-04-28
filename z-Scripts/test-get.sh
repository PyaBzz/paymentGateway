curl --request GET \
--header "Content-Type: application/json" \
--data '{"merchantId":1,"paymentId":2}' \
localhost:5000/payment && echo
