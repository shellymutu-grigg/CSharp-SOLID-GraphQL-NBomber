public static class OrderQueryBuilder
{
    public static string GetOrderById(string id) => $@"
        query GetOrder {{
            order(id: ""{id}"") {{
                orderId
                total
                items
            }}
        }}";
}