// TODO:
// * grab data from api in polling structure


import { useLocation, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";

export function BaristaPage() {
    //////////
    // Data //
    //////////
    const location = useLocation();
    const navigate = useNavigate();
    const username = location.state?.username;
    const [orders, setOrders] = useState(
        [
            {
                items: [
                    { 
                        name: 'smoothie',
                        price: 1.20
                    }, 
                    {
                        name: 'latte',
                        price: 2.10
                    }
                ]
            },
            {
                items: [
                    {
                        name: 'latte',
                        price: 2.10
                    }
                ]
            }
        ]
    )


    //////////////
    // Handlers //
    //////////////
    useEffect(
        () => {
            if (username === undefined) { 
                alert(`The route '${location.pathname}' can only be accessed by authenticated users.\nRedirecting to login page...`)
                navigate("/") 
            }

            orders.forEach((order, orderIdx) => {
                const numItems = order.items.length
                let completeItems = 0
                order.items.forEach((item) => {
                    if (item.complete) { completeItems++ }
                })

                if (completeItems === numItems) {
                    // Remove order from orders and update
                    const newOrders = orders.filter((currOrder) => {
                        return (currOrder !== order)
                    })
                    setOrders(newOrders)
                }
            })
        },
        [username, navigate, location, orders]
    )

    useEffect(
        () => {
            setInterval(
                () => {
                    alert('fetching data...')
                }, 
                15 * 1000
            )
        },
        []
    )

    const handleItemStateChange = (event) => {
        const [orderIdx, itemIdx] = event.target.value.split(",")

        let newOrders = JSON.stringify(orders) 
        newOrders = JSON.parse(newOrders)
        newOrders[orderIdx].items[itemIdx].complete = !newOrders[orderIdx].items[itemIdx].complete ?? true
        setOrders(newOrders)
    }
    
    ////////////////
    // Components //
    ////////////////
    return(
        <>
            <h3>Orders</h3>
            <ol>
            {orders.map((order, orderIdx) =>
                <li>
                    <b>Order Summary</b>
                    <ul>
                    {order.items.map((item, itemIdx) =>
                        <li>
                            {item.name} 
                            <input 
                                key={`${item.name}${orderIdx}${itemIdx}`} 
                                type="checkbox"
                                value={[orderIdx, itemIdx]} 
                                onClick={handleItemStateChange} 
                            />
                        </li>
                    )}
                    </ul>
                </li>
            )}
            </ol>
        </>
    )
}