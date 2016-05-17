import {bootstrap} from 'angular2/platform/browser';
import {HTTP_PROVIDERS} from 'angular2/http';
import { App } from './main';

bootstrap(App, [HTTP_PROVIDERS])
    .then(success => console.log(`Bootstrap success`))
    .catch(error => console.log(error));