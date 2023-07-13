import React, { useState, useEffect } from 'react'
import "./navbar.css"

export default function Navbar() {

    const [date, setDate] = useState(new Date());

    useEffect(() => {
        const timer = setInterval(() => {
            setDate(new Date());
        }, 1000);

        return () => {
            clearInterval(timer);
        };
    }, []);

    const formattedDate = date.toLocaleDateString();
    const formattedTime = date.toLocaleTimeString();

    return (
        <div className='nav p-3'>
            <div style={{ color: "#fff" }} className='center w-50 p-1'>
                <h1>Koç Holding</h1>
                <div>
                    <img className='avatar' src="https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=" />
                    <span className='mx-3'>Name Lastname</span>
                </div>
            </div>
            <div className='right text-center py-3' style={{ color: "#fff" }}>

                <div className='mb-1'>
                    {formattedDate}
                </div>
                <div className='mt-3'>
                    {formattedTime}
                </div>
            </div>
        </div>
    )
}