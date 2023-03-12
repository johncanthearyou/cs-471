// pseudo:
// get list of incomplete orders
// for order in order:
//     Map order to component
//     for item in order.items:
//         Map item to checkable component


import { useLocation, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";

export function BaristaPage() {
    //////////
    // Data //
    //////////
    const location = useLocation();
    const navigate = useNavigate();
    const username = location.state?.username;
    const [orders, setOrders] = useState([{items: [{ name: 'smoothie', price: 1.20 }]} , {items: [{ name: 'latte', price: 2.10 }]}])


    //////////////
    // Handlers //
    //////////////
    useEffect(
        () => {
            if (username === undefined) { 
                alert(`The route '${location.pathname}' can only be accessed by valid users.\nRedirecting to login page...`)
                navigate("/") 
            }
        },
        [username, navigate, location]
    )

    
    ////////////////
    // Components //
    ////////////////
    return(
        <>
            <h3>Orders</h3>
            <ol>
            {orders.map((order, count) => {
                return <li>Order: {count}</li>
            })}
            </ol>
        </>
    )
}