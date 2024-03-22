# Tag2Dxf
Tag2Dxf is a project for deserializing Taglio TAG files and converting them to AutoCAD DXF. TAG is a closed proprietary format, although the files are encoded as plaintext. There aren't any programs that can read them other than Taglio LogoTag (as far as I am aware), and Taglio provides no publicly-facing tools to do bulk conversions.

The objective of this project was to create a small utility that could do these conversions in bulk. The command line interface can take a directory and a recurse argument as parameters in order to process lots of TAG files across multiple directories.
