import MessageContainer from "./MessageContainer";

const Chat = ({ messages }) => <div>
    <div className="chat">
        <MessageContainer messages={messages} />
    </div>
</div>

export default Chat;