const orders = [
    {
        number: 1,
        items: ['item 1', 'item 2']
    },
    {
        number: 2,
        items: ['item 3']
    }
]

export function BaristaPage() {
  return (
    <table>
        <thead>
            <tr>
                <th>NUMBER</th>
                <th>ITEMS</th>
            </tr>
        </thead>
        <tbody>
        {
            orders.map((order) =>
                <tr>
                    <th>{order.number}</th>                    
                    {
                        order.items.map((item) =>
                            <td>{item}</td>
                        )
                    }                   
                </tr>
            )
        }
        </tbody>
    </table>
  );
}