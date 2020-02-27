import * as functions from 'firebase-functions';
import * as twilio from 'twilio';

export const uploadPhoto = functions.https.onRequest((request, response) => {
  const twiml = new twilio.twiml.MessagingResponse();
  twiml.message('Got the text');
  response.writeHead(200, {'Content-Type': 'text/xml'});
  response.end(twiml.toString());
});
