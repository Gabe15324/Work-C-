
import { NavLink } from "react-router-dom";


function Nav() {
    return (
        <nav>
            <ul>
                <li><NavLink to="/">Home</NavLink></li>
                <li><NavLink to="/Cadastros">Cadastros</NavLink></li>
                <li><NavLink to="/pagina2">Pagina2</NavLink></li>
                <li><NavLink to="/pagina3">Página 3</NavLink></li>
            </ul>
        </nav>
    );
}

export default Nav;
