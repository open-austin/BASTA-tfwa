import * as functions from "firebase-functions";
import * as twilio from "twilio";
import * as firebase from "firebase";
import * as extName from "ext-name";
const path = require("path");
const urlUtil = require("url");

async function SaveMedia(mediaItem) {
  const { mediaUrl, filename } = mediaItem;
  if (NODE_ENV !== "test") {
    const fullPath = path.resolve(`${PUBLIC_DIR}/${filename}`);

    if (!fs.existsSync(fullPath)) {
      const response = await fetch(mediaUrl);
      const fileStream = fs.createWriteStream(fullPath);

      response.body.pipe(fileStream);

      deleteMediaItem(mediaItem);
    }

    images.push(filename);
  }
}

export const uploadPhoto = functions.https.onRequest((request, response) => {
  const { body } = request;
  const { NumMedia, From: SenderNumber, MessageSid } = body;

  const mediaItems = [];
  let saveOperations = [];

  for (var i = 0; i < NumMedia; i++) {
    // eslint-disable-line
    const mediaUrl = body[`MediaUrl${i}`];
    const contentType = body[`MediaContentType${i}`];
    const extension = extName.mime(contentType)[0].ext;
    const mediaSid = path.basename(urlUtil.parse(mediaUrl).pathname);
    const filename = `${mediaSid}.${extension}`;

    mediaItems.push({ mediaSid, MessageSid, mediaUrl, filename });
    saveOperations = mediaItems.map(mediaItem => SaveMedia(mediaItem));
  }

  await Promise.all(saveOperations);

  const twiml = new twilio.twiml.MessagingResponse();
  twiml.message("Got the text");
  response.writeHead(200, { "Content-Type": "text/xml" });
  response.end(twiml.toString());

  request.body;

  // Set the configuration for your app
  var firebaseConfig = {
    apiKey: "<your-api-key>",
    authDomain: "<your-auth-domain>",
    databaseURL: "<your-database-url>",
    storageBucket: "<your-storage-bucket-url>"
  };
  firebase.initializeApp(firebaseConfig);

  // Get a reference to the storage service, which is used to create references in your storage bucket
  var storage = firebase.storage();

  functions.storage.bucket();
});
