export function isDark(hex: String) {
  const r = parseInt(hex.substring(1, 3), 16);
  const g = parseInt(hex.substring(3, 5), 16);
  const b = parseInt(hex.substring(5, 7), 16);
  return r * 0.299 + g * 0.587 + b * 0.114 < 120;
}
