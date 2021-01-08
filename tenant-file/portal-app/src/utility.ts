export function isDark(hex: String) {
  const r = parseInt(hex.substring(1, 3), 16);
  const g = parseInt(hex.substring(3, 5), 16);
  const b = parseInt(hex.substring(5, 7), 16);
  return r * 0.299 + g * 0.587 + b * 0.114 < 120;
}

export const sampleData = [
  {
    name: 'Damages',
    description: 'Windows, walls, appliance',
    photoCount: 5,
    id: 0,
    color: '#00D084',
  },
  {
    name: 'Conversations',
    description: 'Records of texts or phone calls',
    photoCount: 4,
    id: 1,
    color: '#0693E3',
  },
  {
    name: 'Documents',
    description: 'Lease, notices, etc.',
    photoCount: 5,
    id: 2,
    color: '#FF6900',
  },
];
