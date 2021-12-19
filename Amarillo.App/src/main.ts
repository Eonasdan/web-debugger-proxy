import App from './App.svelte';
//import {MessageService} from './services/message.service';

const app = new App({
	target: document.body
});

//MessageService.instance = new MessageService();

export default app;