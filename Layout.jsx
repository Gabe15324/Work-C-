import Header from "./Header";
import Nav from "./Nav";
import Aside from "./aside";
import Footer from "./Footer";

function Layout(props) {
    return (
        <div>
            <Header />
            <Nav />
            <Aside />
            <main>{props.children}</main>
            <Footer />
        </div>
    );
}

export default Layout;
