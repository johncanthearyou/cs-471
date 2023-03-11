import { Link, useNavigate, useLocation } from "react-router-dom";
import { useEffect } from "react";

export function WorkerPage() {
    const { state } = useLocation();

    const navigate = useNavigate();
    const username = state?.username;
    const isManager = state?.isManager;
    const roles = ["barista", "cashier"];
    if (isManager) { roles.push("manager"); }

    useEffect(
        () => {
            if (username === undefined) {
                navigate("/");
            }
        },
        [username, navigate]
    )

    return(
        <>
        {(
            roles.map((role) => {
                return (
                    <Link to={`/${role}`} key={role} >{role}<br /></Link>
                );
            })
        )}
        </>
    )
}