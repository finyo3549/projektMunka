import PropTypes from "prop-types";

export function ErrorMessage(props) {
    const { messages } = props;
    return (
        <ul>
            {messages.map(message => <li key={message}>{message}</li>)}
        </ul>
    );

}
ErrorMessage.propTypes = {
    messages: PropTypes.arrayOf(PropTypes.string)
};
