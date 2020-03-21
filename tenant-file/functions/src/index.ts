import * as functions from "firebase-functions";
import * as twilio from "twilio";
// import * as firebase from "firebase";
import * as extName from "ext-name";
import * as admin from "firebase-admin";
// import { Bucket } from "@google-cloud/storage";
const path = require("path");
const urlUtil = require("url");
const fetch = require("node-fetch");

admin.initializeApp();

const bucket = admin.storage().bucket();

const SaveMedia = async ({ filename, mediaUrl, contentType }: MediaItem) => {
  const file = bucket.file(`images/${filename}`);
  console.log(
    await file.getSignedUrl({
      action: "read",
      expires: "03-09-2491"
    })
  );
  const writeStream = file.createWriteStream({ contentType });
  return fetch(mediaUrl).then((res: any) => {
    res.body.pipe(writeStream);
  });
};

type MediaItem = { mediaUrl: string; filename: string; contentType: string };

const processMedia = async (body: any) => {
  const NumMedia = parseInt(body.NumMedia);

  const mediaItems: MediaItem[] = [];
  let saveOperations: Promise<void>[] = [];

  for (let i = 0; i < NumMedia; i++) {
    // eslint-disable-line
    const mediaUrl: string = body[`MediaUrl${i}`];
    const contentType: string = body[`MediaContentType${i}`];
    const extension: string = extName.mime(contentType)[0].ext;
    const mediaSid: string = path.basename(urlUtil.parse(mediaUrl).pathname);
    const filename: string = `${mediaSid}.${extension}`;

    mediaItems.push({ mediaUrl, filename, contentType });
    saveOperations = mediaItems.map(mediaItem => SaveMedia(mediaItem));
  }

  await Promise.all(saveOperations);
};

export const uploadPhoto = functions.https.onRequest(
  async (request, response) => {
    const { body } = request;

    console.log(body);
    await processMedia(body);

    const twiml = new twilio.twiml.MessagingResponse();
    twiml.message("Got the text");
    response.writeHead(200, { "Content-Type": "text/xml" });
    response.end(twiml.toString());
  }
);
