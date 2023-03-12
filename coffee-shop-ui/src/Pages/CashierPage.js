// TODO:
// * get list of available items from API
// * add order to api


import { useEffect, useState } from "react"
import { useLocation, useNavigate } from "react-router-dom"

export function CashierPage() {
    //////////
    // Data //
    //////////
    const location = useLocation()
    const navigate = useNavigate()
    const username = location.state?.username
    const [availableItems] = useState([{ name: 'smoothie', price: 1.20 }, { name: 'latte', price: 2.10 }])
    const [orderItems, setOrderItems] = useState([])
    const [orderTotal, setOrderTotal] = useState(0)
    const [paymentMethod, setPaymentMethod] = useState('')


    //////////////
    // Handlers //
    //////////////
    useEffect(
        () => {
            if (username === undefined) {
                alert(`The route '${location.pathname}' can only be accessed by authenticated users.\nRedirecting to login page...`)
                navigate("/")
            }

            let total = 0
            orderItems.forEach((item) => {
                total += item.price
            })
            setOrderTotal(total.toFixed(2))
        },
        [orderItems, setOrderTotal, navigate, username, location]
    )
     
    const handleAddItem = (event) => {
        const newItemName = event.target.newItem.value.toLowerCase()
        const availableItemNames = availableItems.map((availableItem) => { return availableItem.name })

        const newItemIndex = availableItemNames.indexOf(newItemName)
        if (newItemIndex === -1) {
            // Item is not offered, try again
            alert(`Item '${newItemName}' is not offered. Please enter a valid offered item.`)
        }
        else {
            // Item is offered, add to order
            setOrderItems([...orderItems, availableItems[newItemIndex]])
        }
    }

    const handleRemoveItem = (event) => {
        const indexToRemove = event.target.value
        alert(`Removing item '${orderItems[indexToRemove].name}' from the current order.`)

        const newItems = orderItems.filter((item) => { return (item !== orderItems[indexToRemove]) })
        setOrderItems(newItems)
    }

    const handlePaymentSelection = (event) => {
        const paymentMethod = event.target.value
        setPaymentMethod(paymentMethod)
    }

    const handleSale = (event) => {
        if (paymentMethod === "cash") {
            const cashReceived = event.target.cashReceived.value
            const change = (cashReceived - orderTotal).toFixed(2)
            alert(`Change = $${change}`)
        }
        // TODO: add transaction to db
        
        setOrderItems([])
        setPaymentMethod('')
    }
    

    ////////////////
    // Components //
    ////////////////
    const ItemEntryForm = () => {
        return (
            <form onSubmit={handleAddItem}>
                <label>Enter Item: </label>
                <input type="text" name="newItem" />
                <input type="submit" value="Add Item" />
            </form>
        )
    }

    const PaymentSelectForm = () => {
        return (
            <>
                <label>Payment Method</label>
                <br />
                <button type="button" value="cash" onClick={handlePaymentSelection}>Cash</button>
                <br />
                <button type="button" value="card" onClick={handlePaymentSelection}>Card</button>
            </>
        )
    }

    const CashPaymentForm = () => {
        return (
            <>
                <label>Cash Recieved: </label>
                <input type="number" min={orderTotal} step="0.01" name="cashReceived" />
            </>
        )
    }

    const CardPaymentForm = () => {
        const date = new Date()
        const currentYear = date.getFullYear().toString()
        const currentMonth = (date.getMonth()+1 < 10 ? "0" : "") + (date.getMonth()+1).toString()
        return (
            <>
                <label>Card Number: </label>
                <input type="number" name="cardNumber" min={1000000000000000n} max={9999999999999999n} required={true} />
                <br />
                <label>Expiration Date: </label>
                <input type="month" name="cardExpiration" min={`${currentYear}-${currentMonth}`} required={true} />
                <br />
                <label>Security Number: </label>
                <input type="number" name="cardSecurity" min={100} max={999} required={true} />
            </>
        )
    }

    const PaymentForm = () => {
        if (paymentMethod !== '') {
            return (
                <form onSubmit={handleSale}>
                    {paymentMethod === "card" ? <CardPaymentForm /> : <CashPaymentForm />}
                    <br />
                    <input type="button" value="Return to Payment Select" onClick={() => { setPaymentMethod('') }} />
                    <br />
                    <br />
                    <input type="submit" value="Complete Order" />
                </form>
            )
        }
        return <PaymentSelectForm />
    }

    const OrderDisplay = () => {
        return (
            <>
                <h3>Order Summary</h3>
                <ul>
                    {orderItems.map((item, index) =>
                        <li key={index}>
                            ${item.price.toFixed(2)} - {item.name} <input type="checkbox" value={index} index={index} onClick={handleRemoveItem} />
                        </li>
                    )}
                </ul>
                <h3>Total: ${orderTotal}</h3>
            </>
        )
    }

    return (
        <>
            <ItemEntryForm />
            <OrderDisplay />
            <PaymentForm />
        </>
    )
}