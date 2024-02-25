import PropTypes from "prop-types";
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import "./Registration.css";

export function ErrorMessage(props) {
    const { messages } = props;
    return (
        <ul className="col align-self-center bluebackground text">
            {messages.map(message => <li className="list-group-item listpadding" key={message}>{message}</li>)}
        </ul>
    );

}
ErrorMessage.propTypes = {
    messages: PropTypes.arrayOf(PropTypes.string)
};
