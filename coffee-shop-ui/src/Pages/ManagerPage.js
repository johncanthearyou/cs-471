import { useEffect, useState } from "react"
import { useLocation, useNavigate } from "react-router-dom"

export function ManagerPage() {
    //////////
    // Data //
    //////////
    const navigate = useNavigate()
    const location = useLocation()
    const [username] = useState(location.state?.username)
    const [userPermission, setUserPermission] = useState('')
    

    //////////////
    // Handlers //
    //////////////
    useEffect(
        () => {
            fetch(`localhost:5059/user?username=${username}`)
                .then(result => result.json())
                .then(json => {
                    const permission = json.permission
                    setUserPermission(permission)
                })

            if (username === undefined) { 
                alert(`The route '${location.pathname}' can only be accessed by valid users.\nRedirecting to login page...`)
                navigate("/") 
            }
        },
        [navigate, username, userPermission, location]
    )

    ////////////////
    // Components //
    ////////////////
    return(
        <p>Hello manager {username}</p>
    )
}