import { makeVar } from "@apollo/client";
import { BatchImageData } from "..";

const emptyCart: BatchImageData[] = [
  {
    imageUrl: "",
    phoneNumber: "",
    tenantName: "",
    labels: [
      {
        confidence: 0,
        label: "",
        source: "",
      },
    ],
  },
];

export const imageCartVar = makeVar<BatchImageData[]>(emptyCart);
