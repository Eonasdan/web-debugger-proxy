// noinspection JSDeprecatedSymbols

// Declare the necessary interface for messaging functions
// in the PhotinoWindow application.
declare global {
    interface External {
        sendMessage: (message: string) => void;
        receiveMessage: (delegate: (message: string) => void) => void;
    }

    interface Window {
        External: object;
    }
}

// Make sure that sendMessage and receiveMessage exist
// when the frontend is started without the Photino context.
// I.e. using Vue's `npm run serve` command and hot reload.
if (typeof (window.external.sendMessage) !== 'function') {
    window.external.sendMessage = (message: string) => console.log('Emulating sendMessage.\nMessage sent: ' + message);
}

if (typeof (window.external.receiveMessage) !== 'function') {
    window.external.receiveMessage = (delegate: (message: string) => void) => {
        let message = 'Simulating message from backend.';
        delegate(message);
    };

    window.external.receiveMessage((message: string) => console.log('Emulating receiveMessage.\nMessage received: ' + message));
}

interface PhotinoWindow {
    sendMessage: (message: string) => void;
    receiveMessage: (delegate: (message: string) => void) => void;
}

interface PhotinoMessage {
    Channel: Channel,
    Body?: string,
    Command?: string
}

class MessageService {
    private photinoWindow = window.external as unknown as PhotinoWindow;
    private subscribers: { [key: string]: ((event: any) => void)[] } = {};

    static instance: MessageService;

    constructor() {
        console.log('created message service')
        this.photinoWindow.receiveMessage((message: string) => {
            try {
                const payload = JSON.parse(message) as PhotinoMessage;

                if (!Array.isArray(this.subscribers[payload.Channel])) {
                    return;
                }

                // Trigger callback for each subscriber
                console.log(this.subscribers[payload.Channel])
                this.subscribers[payload.Channel].forEach((callback) => {
                    callback(payload.Body);
                });
            }
            catch (e) {
                console.log(message);
                console.error(e);
            }
        });
    }

    sendMessage(message: PhotinoMessage) {
        this.photinoWindow.sendMessage(JSON.stringify(message));
    }

    subscribe(from: string,
              callbacks: (event: any) => void) {

        if (!Array.isArray(this.subscribers[from])) {
            this.subscribers[from] = [];
        }

        this.subscribers[from].push(callbacks);
    }
}

export const instance =  MessageService.instance = new MessageService();
export enum Channel {
    Proxy = 'Proxy'
}