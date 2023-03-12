import { Link, useNavigate, useLocation } from "react-router-dom"
import { useEffect } from "react"

export function WorkerPage() {
    //////////
    // Data //
    //////////
    const { state } = useLocation()
    const navigate = useNavigate()
    const username = state?.username
    const isManager = state?.isManager
    const roles = ["barista", "cashier"]
    if (isManager) { roles.push("manager") }

    //////////////
    // Handlers //
    //////////////
    useEffect(
        () => { if (username === undefined) { navigate("/") } },
        [username, navigate]
    )

    ////////////////
    // Components //
    ////////////////
    return(
        <>
        {(
            roles.map((role) => {
                return (
                    <Link to={`/${role}`} key={role} state={{ username: username }}>{role}<br /></Link>
                )
            })
        )}
        </>
    )
}