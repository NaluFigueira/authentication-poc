const crypto = require("crypto");
const base64url = require("base64url");

const header = { alg: "HS256", typ: "JWT" };
const encodedHeader = base64url(JSON.stringify(header));

const payload = {
  iss: "website.com.br", // where the token is coming from
  iat: new Date().toLocaleString(), // creation timestamp
  exp: new Date().setMinutes(60).toLocaleString(), // expiration timestamp
  acl: ["teacher"],
  username: "Rayssa da Costa",
  email: "rayssa_sabrina_dacosta@baltico.com.br",
};

const encodedPayload = base64url(JSON.stringify(payload));

// secret generated with https://passwordsgenerator.net/sha256-hash-generator/
const secret =
  "46070D4BF934FB0D4B06D9E2C46E346944E322444900A435D7D9A95E6D7435F5";
const data = `${encodedHeader}.${encodedPayload}`;
const signature = crypto
  .createHmac("sha256", secret)
  .update(data)
  .digest("base64");
const encodedSignature = base64url.fromBase64(signature);

const token = `${data}.${encodedSignature}`;

//check your token at https://jwt.io/
console.log(token);
