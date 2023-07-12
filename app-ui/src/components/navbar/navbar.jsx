import React from 'react'
import { Image } from 'semantic-ui-react'
export default function Navbar() {
    return (
        <div className='top_bar' >

            <div style={{ display: "flex", alignItems: "center", borderLeft: '3px solid white', height: '50px', padding: "10px" }}>
                <div style={{ width: "36px", height: "36px", borderRadius: "50%", overflow: "hidden", marginRight: "10px" }}>
                    <Image src={'https://img.a.transfermarkt.technology/portrait/big/28003-1671435885.jpg?lm=1'} style={{ objectFit: "cover", width: "100%", height: "100%" }} />
                </div>
                <div className='name'>Lionel Messi</div>
            </div>
        </div>

    )
}