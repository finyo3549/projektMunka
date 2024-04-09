import "../standards.css";

function GamePage() {
    return (
        <div className="">
            <div className="container text-center">
                <div style={{padding: "2%" , marginBottom: "1%"}} className="row">
                    <div className="col">
                        <button className="buttonstandards">Segítség 1</button>
                    </div>
                    <div className="col">
                        <button className="buttonstandards">Segítség 2</button>
                    </div>
                    <div className="col">
                        <button className="buttonstandards"> Segítség 3</button>
                    </div>
                </div>
                <div style={{padding: "2%", borderRadius: "100px" }} className="bluebackground">Kérdés szövege</div>
                <div style={{margin: "5%"}} className="row">
                    <div className="col-sm "><button className="buttonstandards">A válasz</button></div>
                    <div className="col-sm"></div>
                    <div className="col-sm "><button className="buttonstandards">A válasz</button></div>
                </div>
                <div style={{margin: "5%"}} className="row">
                    <div className="col-sm "><button className="buttonstandards">A válasz</button></div>
                    <div className="col-sm"></div>
                    <div className="col-sm "><button className="buttonstandards">A válasz</button></div>
   
                </div>

            </div>
        </div>
    );
}

export default GamePage;