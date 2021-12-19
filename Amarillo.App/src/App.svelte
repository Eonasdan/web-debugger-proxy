<script lang="ts">
    import {Channel, instance as MessageService} from './services/message.service';

    let state = false;
    let entries = [];

    MessageService.subscribe(Channel.Proxy, (message) => {
        entries = [...entries, message];
    });

    const toggle = () => {
        state = !state;
        MessageService.sendMessage({
            Channel: Channel.Proxy,
            Command: state ? 'start' : 'stop'
        });
    }
</script>
<button class="btn btn-success" on:click={toggle}>Toggle</button>
<div class="container-fluid">
    <div class="row">
        <div class="col">
            <table class="table table-striped">
                <thead>
                <tr>
                    <td>Url</td>
                    <td>Result</td>
                    <td>Method</td>
                    <td>Remote Address</td>
                </tr>
                </thead>
                {#each entries as item}
                    <tr>
                        <td>{item.Url}</td>
                        <td>{item.Result}</td>
                        <td>{item.Method}</td>
                        <td>{item.RemoteAddress}</td>
                    </tr>
                {/each}
            </table>
        </div>
    </div>
</div>